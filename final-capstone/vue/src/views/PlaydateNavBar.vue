<template>
  <div>
    <v-container class="d-flex justify-space-around flex-wrap" outline>
    <v-card>
        <router-link v-bind:to="{ name: 'create-playdate-form' }">
          <v-btn>Add Playdate</v-btn>
        </router-link>
      </v-card>
      <v-card>
        <router-link v-bind:to="{ name: 'user-playdate-view-in-progress' }">
          <v-btn>View In Progress Playdates</v-btn>
        </router-link>
      </v-card>

      <v-card
        ><router-link v-bind:to="{ name: 'user-playdate-view-completed' }"
          ><v-btn>View Completed Playdates</v-btn></router-link
        ></v-card
      >
    </v-container>
  </div>
</template>

<script>
export default {
  name: "playdate-nav-bar",
};
</script>

<style>
</style> 