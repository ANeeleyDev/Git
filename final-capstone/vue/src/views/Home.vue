<template>
  <div class="home">
    <p>{{ this.$store.state.userProfile.firstName }}</p>
    <nav-bar />
    <pet-list />
  </div>
</template>

<script>
import PetList from "./PetList.vue";
import userService from "@/services/UserService";
import NavBar from "./NavBar.vue";
export default {
  components: {
    PetList,
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
