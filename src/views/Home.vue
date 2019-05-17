<template>
  <div class="home">
    <Value msg="Value Component"/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import Value from '@/features/value/Value.vue'

@Component({
  components: {
    Value,
  },
})
export default class Home extends Vue {}
</script>
