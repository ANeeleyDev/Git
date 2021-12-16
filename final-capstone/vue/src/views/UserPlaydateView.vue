<template>
  <div>
      <playdate-nav-bar v-if="$store.state.token != ''" />
      <user-playdate-list />
  </div>
</template>

<script>
import UserPlaydateList from '@/views/UserPlaydateList'
import PlaydateNavBar from "./PlaydateNavBar.vue";

export default {
  components: { UserPlaydateList, PlaydateNavBar },
    name: 'user-playdate-view'
}
</script>

<style>

</style>