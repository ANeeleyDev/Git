<template>
  <div>
    <v-container v-if="this.$store.state.token === ''" class="d-flex justify-space-around flex-wrap" outline>
      <v-card>
        <router-link v-bind:to="{ name: 'login' }">
          <v-btn>Login</v-btn>
        </router-link>
      </v-card>
      <v-card>
        <router-link v-bind:to="{ name: 'register' }">
          <v-btn>Register</v-btn>
        </router-link>
      </v-card>
      
    </v-container>

    <v-container v-else class="d-flex justify-space-around flex-wrap" outline>
      <v-card>
        <router-link v-bind:to="{ name: 'profile-details' }">
          <v-btn>View My Profile</v-btn>
        </router-link>
      </v-card>
      <v-card>
        <router-link v-bind:to="{ name: 'pet-view' }">
          <v-btn>View My Pets</v-btn>
        </router-link>
      </v-card>
      
      <v-card><router-link v-bind:to="{ name: 'user-playdate-view' }"><v-btn>My Playdates</v-btn></router-link></v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "nav-bar",
};
</script>

<style>
</style>