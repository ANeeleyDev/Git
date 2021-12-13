<template>
  <div>
    <h2>{{this.$store.state.userProfile.firstName}} {{this.$store.state.userProfile.lastName}}</h2>

    <p>{{this.$store.state.userProfile.emailAddress}} {{this.$store.state.userProfile.phoneNumber}}</p>
    <p>{{this.$store.state.userProfile.streetAddress}}</p>
    <p>{{this.$store.state.userProfile.city}}, {{this.$store.state.userProfile.state}} {{this.$store.state.userProfile.zip}}</p>

     
    <router-link :to="{ name: 'register-user' }"><v-btn>Edit Profile</v-btn></router-link>
  
  </div>
 
</template>

<script>
export default {
  name: "profile-details",
};
</script>

<style>
</style>