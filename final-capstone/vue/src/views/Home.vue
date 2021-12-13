<template>
  <div class="home">
    <nav-bar />
    <playdate-view/>
   
  </div>
</template>

<script>

import PlaydateView from './PlaydateView.vue';
import userService from "@/services/UserService";
import NavBar from "./NavBar.vue";
export default {
  components: {
    PlaydateView,
    NavBar,
  },
  name: "home",

  methods: {
    getCurrentUserProfile() {
      userService.displayUserProfile().then((response) => {
        this.$store.commit("SET_USER_PROFILE", response.data);
      });
    },
  },
  created() {
    this.getCurrentUserProfile();
  },
};
</script>
