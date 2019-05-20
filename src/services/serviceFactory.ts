import ValueService from "../features/value/valueService"
import UserService from "../features/user/userService"

const services: any = {    
    value: ValueService,
    user: UserService,
};

export const ServiceFactory = {
    get: (name: string) => {
        return services[name];
    }
};
