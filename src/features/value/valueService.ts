import Axios from "../../services/axios"

const resource = "/value"
export default class valueService {
    get() {
        return Axios.get(`${resource}`)
    }

    getValue(valueId: number) {
        return Axios.get(`${resource}/${valueId}`)
    }

    postValues(values: number[]) {
        return Axios.post(`${resource}`, values)
    }
}