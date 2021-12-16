<template>
  <div>
      <playdate-nav-bar v-if="$store.state.token != ''" />
      <user-playdate-list-in-progress />
  </div>
</template>

<script>
import UserPlaydateListInProgress from './UserPlaydateListInProgress.vue';
import PlaydateNavBar from "./PlaydateNavBar.vue";

export default {
  components: { UserPlaydateListInProgress, PlaydateNavBar },
    name: 'user-playdate-view-in-progress'
}
</script>

<style>

</style>