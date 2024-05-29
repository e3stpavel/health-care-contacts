import { z } from 'astro/zod'

export const errorSchema = z.object({
  type: z.string().url(),
  title: z.string(),
  status: z.number().gte(400).lt(600),
  traceId: z.string(),
})
