import axios from "axios"

const baseDomain = ""
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL
})