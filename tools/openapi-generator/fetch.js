import { existsSync, mkdirSync } from 'fs'
import { writeFile } from 'fs/promises'
import { Agent } from 'https'
import fetch from 'node-fetch'
import { join } from 'path'
import { DOC_DIR, workspace } from './constant.js'

// to fix fetch issue that complains "reason: self signed certificate"
// https://stackoverflow.com/questions/52478069/node-fetch-disable-ssl-verification
const agent = new Agent({
  rejectUnauthorized: false,
})

const fetchDoc = async (url) =>
  fetch(url, { method: 'GET', agent }).then((res) => {
    if (!res.ok)
      throw new Error(`Fetch openapi document failed: ${res.statusText}`)
    return res.text()
  })

const writeToPath = (name, content) => {
  const filePath = join(DOC_DIR, name + '.json')
  return writeFile(filePath, content, { encoding: 'utf8' })
}

const fetchTasks = Object.entries(workspace).map(([name, url]) =>
  fetchDoc(url)
    .then((content) => writeToPath(name, content))
    .catch((err) => {
      console.log(`Failed to fetch opanapi document for ${name}:`)
      console.log(err.message)
    })
)

if (!existsSync(DOC_DIR)) {
  mkdirSync(DOC_DIR)
}
Promise.allSettled(fetchTasks)
