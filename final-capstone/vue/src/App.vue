
<template>
  <v-app>
    <!-- Must have the app property -->
    <v-navigation-drawer v-model="drawer" app>
      <v-sheet color="grey lighten-4" class="pa-4">
        <v-avatar class="mb-4" color="grey darken-1" size="64"></v-avatar>
        <div v-if="$store.state.token === ''">Welcome, Guest</div>
        <div v-else>
        <div>{{this.$store.state.userProfile.firstName}} {{this.$store.state.userProfile.lastName}}</div>
        <div>{{this.$store.state.userProfile.emailAddress}}</div>
        </div>
        
      </v-sheet>
      <v-divider></v-divider>
      <v-list>
        <v-list-item>
           <router-link v-bind:to="{ name: 'home' }">
            <v-btn rounded color="primary" dark> Home </v-btn>
          </router-link>
        </v-list-item>

        <v-list-item>
          <router-link
            v-bind:to="{ name: 'login' }"
            v-if="$store.state.token === ''"
            ><v-btn rounded color="primary" dark> Login </v-btn></router-link
          >
           <router-link
            v-bind:to="{ name: 'logout' }"
            v-else
            ><v-btn rounded color="primary" dark> Logout </v-btn></router-link
          >
        </v-list-item>
        <v-list-item>
          <router-link v-bind:to="{ name: 'register-user' }">
            <v-btn rounded color="primary" dark> Register</v-btn>
          </router-link>
        </v-list-item>
      
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>PetsOnly</v-toolbar-title>
      
      

      <v-spacer></v-spacer>
      <router-link v-bind:to="{ name: 'home' }">
        <v-btn rounded color="primary" dark> Home </v-btn>
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
