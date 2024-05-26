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

export const selectFacilitySchema = z.object({
  id: z.coerce.number().positive(),
  description: z.string().min(3),
  type: z.enum(facilityTypes),

  // TODO: right now every time it is submitted, new facility type is created
  facilityTypeDescription: z.string(),

  contacts: contactsSchema.default([]),
})

export type SelectFacility = z.infer<typeof selectFacilitySchema>

export const insertFacilitySchema = selectFacilitySchema.omit({ id: true })

export type InsertFacility = z.infer<typeof insertFacilitySchema>
