import { writeFileSync } from 'fs'
import { jsonSchemaToZod } from 'json-schema-to-zod'
import { join } from 'path'
import { SHARED_SCHEMA_NAME } from '../constant.js'

export const genZodSchema = (document, target) => {
  const schemas = document.components.schemas || {}
  const schemaPath = join(target.output, 'schema.ts')

  let content = Object.entries(schemas)
    .filter(([name, _]) => !SHARED_SCHEMA_NAME.includes(name))
    .map(([name, schema]) => jsonSchemaToZod(schema, name, false))
    .map((c) => `export ${c}`)
    .join('\n')

  content = 'import { z } from "zod"\n\n' + content

  writeFileSync(schemaPath, content, { encoding: 'utf8' })
}
