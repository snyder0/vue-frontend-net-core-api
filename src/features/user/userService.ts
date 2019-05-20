import Axios from "../../services/axios"

const resource = "/Users"
export default class userService {
    get() {
        return Axios.get(`${resource}`)
    }

    getUser(userId: number) {
        return Axios.get(`${resource}/${userId}`)
    }

    postUsers(users: object) {
        return Axios.post(`${resource}`, users)
    }
}