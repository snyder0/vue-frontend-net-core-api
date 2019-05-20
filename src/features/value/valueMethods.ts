import { ServiceFactory } from '../../services/serviceFactory'
const ValueService = ServiceFactory.get('value')

export default class ValueMethods {
    public get(): number[] {
        return ValueService.get()
    }

    public getValue(valueId: number): number {
        return ValueService.getValue(valueId)
    }

    public addValue(values: number[]): void {
        ValueService.postValues(values)
    }
}