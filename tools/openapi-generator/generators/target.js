import { existsSync, mkdirSync, readFileSync } from 'fs'
import { join } from 'path'
import { genAggregation } from './module-aggregation.js'
import { genZodSchema } from './zod-schema.js'

const checkDir = (target) => {
  if (!existsSync(target.input)) {
    throw new Error(
      `The openapi document source for target "${target.name}" does not exist`
    )
  }

  if (!existsSync(target.output)) {
    mkdirSync(target.output, { recursive: true })
  }
}

export const genTarget = (target) => {
  checkDir(target)

  const content = readFileSync(target.input, { encoding: 'utf8' })
  const document = JSON.parse(content)

  genZodSchema(document, target)
  genAggregation(['schema'], join(target.output, 'index.ts'))
}
