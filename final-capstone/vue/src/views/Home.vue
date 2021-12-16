<template>
  <div class="home">
    
    <nav-bar v-if="$store.state.token != ''" />
    <v-container>
      <v-row class="text-center">
        <v-col cols="12">
          <v-img
            src="https://d33wubrfki0l68.cloudfront.net/56ffdbe7fabe864ed466a5b9c100455f1dece75e/866b2/static/tdcw-bc-back-white-4e46761f5a4842e1bd81a20ca5b39315.png"
            class="my-3"
            contain
            height="200"
          />
        </v-col>

        <v-col class="mb-4">
          <h1 class="display-2 font-weight-bold mb-3">Welcome to PetsOnly</h1>

          <p>Is Spike lonely? He doesn't have to be...</p>
        </v-col>
      </v-row>
    </v-container>
    

    <div class="playdate-list">
      <v-card flat tile>
        <v-toolbar color="cyan" dark>
          <v-col cols="5"></v-col>
          <v-col class="d-flex justify-space-around">
            <v-app-bar-title class="white--text">
              Look Who's Ready to Play!</v-app-bar-title>
            
          </v-col>
          <v-col cols="5" class="d-flex justify-end"></v-col>
          <v-spacer></v-spacer>
        </v-toolbar>
      </v-card>
    </div>
    <playdate-view align="center" />
  </div>
</template>

<script>
import PlaydateView from "./PlaydateView.vue";
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
