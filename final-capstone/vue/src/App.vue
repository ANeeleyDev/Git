
<template>
  <v-app>
    <!-- Must have the app property -->

    <v-app-bar app>
      

      <v-toolbar-title>PetsOnly</v-toolbar-title>

      <v-spacer></v-spacer>
      <router-link v-bind:to="{ name: 'home' }">
        <v-btn rounded color="primary" dark> Home </v-btn>
      </router-link>
      <router-link v-if="$store.state.token === ''" v-bind:to="{ name: 'register-user' }">
        <v-btn rounded color="primary" dark> Register</v-btn>
      </router-link>
      <router-link
        v-bind:to="{ name: 'logout' }"
        v-if="$store.state.token != ''"
        ><v-btn rounded color="primary" dark> Logout </v-btn></router-link
      >
      <router-link
        v-bind:to="{ name: 'login' }"
        v-if="$store.state.token === ''"
        ><v-btn rounded color="primary" dark> Login </v-btn></router-link
      >
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>
<script>
export default {
  data() {
    return {
      drawer: null,
    };
  },
};
</script>
