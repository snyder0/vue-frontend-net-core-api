import ValueService from "../features/value/valueService"

const services: any = {    
    value: ValueService,
};

export const ServiceFactory = {
    get: (name: string) => {
        return services[name];
    }
};
