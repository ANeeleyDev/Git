<template>
  
    <v-container color="blue" class="d-flex justify-space-around flex-wrap" outline>
        <v-card>
          <router-link v-bind:to="{name:'profile-details'}">
            <v-btn>View My Profile</v-btn>
          </router-link>
          </v-card>
        <v-card><v-btn>View My Pets</v-btn></v-card>
        <v-card><v-btn>Forum</v-btn></v-card>    
        
    </v-container>
 
</template>

<script>
export default {
  name: "nav-bar",
};
</script>

<style>
</style>