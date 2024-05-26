import { z } from 'astro/zod'
import { mutate, query } from './common'
import type { InsertFacility, SelectFacility } from '~/schemas/facility'
import { selectFacilitySchema } from '~/schemas/facility'

const path = 'facilities'

type FacilityId = SelectFacility['id']

async function findAll() {
  return await query(path, z.array(selectFacilitySchema))
}

async function findOne(id: FacilityId) {
  return await query(`${path}/${id}`, selectFacilitySchema)
}

async function add(facility: InsertFacility) {
  return await mutate(path, facility, selectFacilitySchema)
}

async function update(id: FacilityId, facility: SelectFacility) {
  return await mutate(`${path}/${id}`, facility, selectFacilitySchema, {
    method: 'PUT',
  })
}

async function remove(id: FacilityId) {
  return await mutate(`${path}/${id}`, {}, z.object({}), {
    method: 'DELETE',
  })
}

export default {
  findAll,
  findOne,
  add,
  update,
  remove,
}
