<template>
    <h1>{{ msg }}</h1>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { ServiceFactory } from "../../services/serviceFactory"
const ValueService = ServiceFactory.get('value')

@Component
export default class Value extends Vue {
    @Prop() private msg!: string;

    // mounted() {
    //     ValueService.get();
    // }
}
</script>