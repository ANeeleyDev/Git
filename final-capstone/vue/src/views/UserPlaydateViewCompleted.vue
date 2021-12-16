<template>
  <div>
      <playdate-nav-bar v-if="$store.state.token != ''" />
      <user-playdate-list-completed />
  </div>
</template>

<script>
import UserPlaydateListCompleted from './UserPlaydateListCompleted.vue';
import PlaydateNavBar from "./PlaydateNavBar.vue";

export default {
  components: { UserPlaydateListCompleted, PlaydateNavBar },
    name: 'user-playdate-view-completed'
}
</script>

<style>

</style>