import { z } from 'astro/zod'
import { contactsSchema } from './contacts'

export const facilityTypes = [
  'ambulatorySurgeryCenter',
  'clinic',
  'floor',
  'hospital',
  'medicalBuilding',
  'medicalOffice',
  'room',
] as const

export const facilitySchema = z.object({
  description: z.string().min(3),
  type: z.enum(facilityTypes),

  // TODO: kinda problem spot
  facilityTypeDescription: z.string(),

  contacts: contactsSchema.default([]),
})

export type Facility = z.infer<typeof facilitySchema>
