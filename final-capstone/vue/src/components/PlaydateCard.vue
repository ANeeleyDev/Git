<template>
  <!-- <v-card
    class="mx-auto"
    max-width="400"
    >
    <vtitle>{{playdate.meetingTime}}</vtitle>
    <v-card-text class="pd-comments">
        <div>C {{playdate.playdateCity}}, {{playdate.playdateState}} {{playdate.playdateZip}}</div>
    </v-card-text>
    <v-img class="pd-pet-pic" height="200px"
        src={{pet.petImage}} >

    <v-card-title>
        Come Pay With Me!
    </v-card-title>

    <v-card-subtitle class="pd-name">
        Name {{pet.petname}}
    </v-card-subtitle>
    <v-card-subtitle class="pd-age">
        Age {{pet.age}}
    </v-card-subtitle>

    <v-card-subtitle class="pd-breed">
        Breed {{pet.breed}}
    </v-card-subtitle>

    <v-card-text class="pd-comments">
        <div>{{pet.otherComments}}</div>
    </v-card-text>

    <v-card-actions>
        <v-btn
            color="green"
            text
        >
        Request Playdate
        </v-btn>

        <v-btn
            color="blue"
            text
        >
        Save 
        </v-btn>
    </v-card-actions>
 </v-card> -->
  <v-card :loading="loading" class="mx-auto my-12" max-height=auto  width="500">
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>

    <!-- <v-img
      height="250"
      :src="this.pet.petImage"
    ></v-img> -->

    <v-card-title>{{this.pet.petName}} & {{this.user.firstName}}</v-card-title>
    <v-card-text>{{this.playdate.playdateAddress}}
    
    {{this.playdate.playdateCity}}, {{playdate.playdateState}} {{playdate.playdateZip}}</v-card-text>
    <v-card-text>{{this.playdate.meetingTime}}</v-card-text>


    <v-card-actions>
      <!-- <v-btn color="deep-purple lighten-2" text @click="reserve">
        Reserve
      </v-btn> -->
    </v-card-actions>
  </v-card>
</template>

<script>
import petService from "@/services/PetService";
import userService from '@/services/UserService';
export default {
  name: "playdate-card",
  props: ["playdate"],
  data() {
    return {
      pet: {},
      user:{}
    };
  },
  methods: {
    getPetInfo() {
      petService.getPet(this.playdate.playdatePostedPetId).then((response) => {
       
          this.pet = response.data;
        })
      
    },
     getUserInfo() {
      userService.displayUser(this.playdate.playdatePostedUserId).then((response) => {
       
          this.user = response.data;
        })
      
    },
  },
  created() {
      this.getPetInfo();
      this.getUserInfo();
  },
};
</script>