<template>
  <div>
      <router-link :to="{ name: 'create-playdate-form' }"><v-btn>Add Playdate</v-btn></router-link>
      <user-playdate-list />
  </div>
</template>

<script>
import UserPlaydateList from '@/views/UserPlaydateList'

export default {
  components: { UserPlaydateList },
    name: 'user-playdate-view'
}
</script>

<style>

</style>