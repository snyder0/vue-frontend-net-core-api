<template>
  <div class="users">
    <User msg="User Component"/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import User from '@/features/user/user.vue'

@Component({
  components: {
    User,
  },
})
export default class Home extends Vue {}
</script>
