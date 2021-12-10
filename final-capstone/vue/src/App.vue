
<template>
  <v-app>
    <!-- Must have the app property -->
    <v-app-bar app>
      <div class="logo">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://pupuphooray.com/wp-content/uploads/2019/03/dog-icon.png"
          transition="scale-transition"
          width="40"
        />
      </div>
      <div class="title">TEST</div>
      <v-spacer></v-spacer>
      <router-link v-bind:to="{ name: 'home' }">
        <v-btn rounded color="primary" dark> Home </v-btn>
      </router-link>
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''"><v-btn rounded color="primary" dark> logout </v-btn></router-link>
      
    </v-app-bar>
   
    <v-main>
      <v-row>
        <v-col>
      <router-view />
      </v-col>
      </v-row>
    </v-main>
  </v-app>
</template>
<!--
<div id="app">
<div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
       </div> -->