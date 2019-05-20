<template>
	<h1>{{ msg }}</h1>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import valueMethods from './valueMethods'
const ValueMethods = new valueMethods()

@Component
export default class Value extends Vue {
    @Prop() private msg!: string;

    mounted() {
        ValueMethods.get()
	}
	
	getValue(valueId: number) {
		ValueMethods.getValue(valueId)
	}

	addValue(values: number[]) {
		ValueMethods.addValue(values)
	}
}
</script>