import { writeFileSync } from 'fs'

export const genAggregation = (targets, output) => {
  const content = targets
    .map((target) => `export * from './${target}'`)
    .join('\n\n')

  writeFileSync(output, content, { encoding: 'utf8' })
}
