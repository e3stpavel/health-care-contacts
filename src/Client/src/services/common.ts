import type { z } from 'astro/zod'
import { errorSchema } from '~/schemas/error'

export async function query<T extends z.ZodTypeAny>(path: string, schema: T) {
  const response = await useAPI(schema, `${import.meta.env.API}/${path}`, {
    headers: {
      Accept: 'application/json',
    },
  })

  return response
}

interface MutateOptions {
  method: 'POST' | 'PUT' | 'DELETE'
}

export async function mutate<T extends z.ZodTypeAny, U extends Record<string, unknown>>(
  path: string,
  body: U,
  schema: T,
  options?: MutateOptions,
) {
  const { method = 'POST' } = options ?? {}
  const response = await useAPI(schema, `${import.meta.env.API}/${path}`, {
    method,
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(body),
  })

  return response
}

async function useAPI<T extends z.ZodTypeAny>(schema: T, ...args: Parameters<typeof fetch>) {
  const response = await fetch(...args)
  const data = await response.json()

  if (!response.ok)
    return { ok: false as const, error: await errorSchema.parseAsync(data) }

  return { ok: true as const, data: await schema.parseAsync(data) as z.infer<T> }
}
