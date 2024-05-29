import { z } from 'astro/zod'

const electronicAddressSchema = z.object({
  uri: z.union([
    z.string().url(),
    z.string().email(),
  ]),
})

const postalAddressSchema = z.object({
  addressLine1: z.string(),
  addressLine2: z.string().optional(),
})

const telecommunicationsSchema = z.object({
  areaCode: z.string().min(2).max(3).toUpperCase(),
  contactNumber: z.string().min(8).max(16),
  countryCode: z.string().min(2).max(3).optional(),
})

export const contactsSchema = z.array(
  z.union([
    electronicAddressSchema,
    postalAddressSchema,
    telecommunicationsSchema,
  ]),
)
