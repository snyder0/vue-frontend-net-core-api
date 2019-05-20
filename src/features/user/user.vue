<template>
    <div>
        <h1>{{ msg }}</h1>
        <div>
            <label for="firstName">First Name</label>
            <input id="firstName" v-model="user.firstName"/>
            <br/>
            <label for="lastName">Last Name</label>
            <input id="lastName" v-model="user.lastName"/>
            <br/>
            <label for="email">Email</label>
            <input id="email" v-model="user.email"/>
            <br/>
            <label for="password">Password</label>
            <input id="password" v-model="user.password"/>
            <br/>
            <button v-on:click="createUser()">Add User</button>
        </div>
        <table>
            <tr>
                <th>Id</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Email</th>
            </tr>
            <tr v-for="(user, index) in users" :key="index">
                <td>{{user.id}}</td>
                <td>{{user.firstName}}</td>
                <td>{{user.lastName}}</td>
                <td>{{user.email}}</td>
            </tr>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import userService from "./userService"
const UserService = new userService()

@Component
export default class User extends Vue {
    @Prop() private msg!: string;

    users: any = null
    user: object = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
    }
    

    async mounted() {
        let response = await UserService.get()
        this.users = response.data.data
    }
    
    async createUser() {
        let response = await UserService.postUsers(this.user)
        this.users.push(response.data.data)
    }
}
</script>