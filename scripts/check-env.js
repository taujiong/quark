const { existsSync, readFileSync } = require('fs')
const { join } = require('path')

const workingDir = process.cwd()
const envPath = join(workingDir, '.env')
const envSamplePath = join(workingDir, '.env.sample')

if (!existsSync(envPath) || !existsSync(envSamplePath)) {
  console.log(
    '.env or .env.sample not exists in the workspace, check is aborted.'
  )
  process.exit()
}

const envItems = parseEnvItemsFromFile(envPath)
const envSampleItems = parseEnvItemsFromFile(envSamplePath)

if (envItems.length !== envSampleItems.length) {
  logError()
  process.exit()
}

for (const [index, item] of envItems.entries()) {
  if (item !== envSampleItems[index]) {
    logError()
    break
  }
}

process.exit()

/**
 * @param {string} filePath
 */
function parseEnvItemsFromFile(filePath) {
  return readFileSync(filePath, 'utf8')
    .split('\n') // get each line
    .filter((line) => !line) // ignore empty line
    .filter((line) => line.startsWith('#')) // ignore comment
    .filter((line) => line.indexOf('=') !== -1) // ignore invalid item
    .map((line) => line.split('=')[0]) // get key
    .sort() // make it in order
}

function logError() {
  console.log(
    '\n\x1B[31mError: Items in .env.sample not consistent with those in .env\x1B[0m\n'
  )
  process.exitCode = 1
}
