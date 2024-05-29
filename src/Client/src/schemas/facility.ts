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
  facilityTypeDescription: z.string().min(5),

  squareFootage: z.preprocess(
    x => x === '' ? undefined : x,
    z.coerce.number().positive().max(1000).optional(),
  ),
  // partOfFacility: z.lazy(() => selectFacilitySchema),
  contacts: contactsSchema.default([]),
})

export type Facility = z.infer<typeof selectFacilitySchema>

export const insertFacilitySchema = selectFacilitySchema.omit({ id: true })

export type NewFacility = z.infer<typeof insertFacilitySchema>
