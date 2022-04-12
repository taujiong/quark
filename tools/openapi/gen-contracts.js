import { join } from 'path'
import { DOC_DIR, OUTPUT_DIR, workspace } from './constant.js'
import { genAggregation } from './generators/module-aggregation.js'
import { genTarget } from './generators/target.js'

const targets = Object.keys(workspace)
targets
  .map((target) => ({
    name: target,
    input: join(DOC_DIR, target + '.json'),
    output: join(OUTPUT_DIR, target),
  }))
  .forEach(genTarget)

genAggregation(targets, join(OUTPUT_DIR, 'index.ts'))
