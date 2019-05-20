import axios from "axios"

const baseDomain = "http://localhost:50301"
const baseURL = `${baseDomain}/api`

export default axios.create({
    baseURL
})