<template>

    
  <v-card class="mx-auto my-12" max-height=auto  width="500">
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>

   <v-img
      height="250"
      :src="this.pet.petImage"
    ></v-img>

    <v-card-title>Meet {{this.pet.petName}}</v-card-title>
    <v-card-subtitle v-if="this.playdate.playdateStatusId === 1 && this.playdate.playdatePostedUserId === this.$store.state.user.userId"> 
      {{this.requestedPet.petName}} wants to meet you!</v-card-subtitle>
      <v-btn 
        color="green" 
        text v-if="this.playdate.playdateStatusId === 1 && this.playdate.playdatePostedUserId === this.$store.state.user.userId" 
        @click="acceptPlaydate"
        > 
        Approve 
      </v-btn>
      <v-btn 
        color="red darken-4" 
        text v-if="this.playdate.playdateStatusId === 1 && this.playdate.playdatePostedUserId === this.$store.state.user.userId" 
        @click="rejectPlaydate"
        > 
        Reject 
      </v-btn>
      <v-btn
          text v-if="this.playdate.playdateStatusId === 1 && this.playdate.playdatePostedUserId === this.$store.state.user.userId" 
          color="orange accent-4"
          @click="revealRequestedPet = !revealRequestedPet"
        >
          About {{this.requestedPet.petName}}
        </v-btn> 

    <v-card-text>{{this.playdate.playdateAddress}}
    
    <br/>{{this.playdate.playdateCity}}, {{playdate.playdateState}} {{playdate.playdateZip}}</v-card-text>
    <v-card-text>{{this.playdate.meetingTime}}</v-card-text>


    <v-card-actions>
        <v-btn
          text v-if="this.playdate.playdatePostedUserId !== this.$store.state.user.userId" 
          color="orange accent-4"
          @click="reveal = !reveal"
        >
          Learn More About {{this.pet.petName}}
        </v-btn> 
        <v-btn
          text v-if="this.playdate.playdatePostedUserId !== this.$store.state.user.userId" 
          color="red"
          @click="$router.push({name: 'request-playdate', params: {playdateId: playdate.playdateId}})"
        >
        Request Playdate
        </v-btn>

        <v-btn
            color="blue"
            text v-if="this.playdate.playdatePostedUserId === this.$store.state.user.userId" 
            @click="$router.push({name: 'edit-playdate', params: {playdateId: playdate.playdateId}})"
        >
        Edit 
        </v-btn>

        <v-btn 
        color="red" 
        text v-if="this.playdate.playdatePostedUserId === this.$store.state.user.userId" 
        @click="deletePlaydate"
        > 
        Delete 
        </v-btn>

    </v-card-actions>

    <v-expand-transition>
      <v-card
        v-if="reveal"
        class="transition-fast-in-fast-out v-card--reveal"
        style="height: 100%;"
      >

        <v-card-text class="pb-0">
          <p class="text-h4 text--primary">
            {{ pet.petName }}
          </p>
            
          <p> {{pet.species}}, {{pet.breed}}, {{pet.age}} years old</p>
          <p style="display:inline" >Attributes: </p>
          <p style="display:inline" v-if="pet.playful === true" >Playful</p>
          <p style="display:inline" v-if="pet.nervous === true & pet.playful === true">, </p>
          <p style="display:inline" v-if="pet.nervous === true" >Nervous</p>
          <p style="display:inline" v-if="pet.confident === true">, </p>
          <p style="display:inline" v-if="pet.confident === true" >Confident</p>
          <p style="display:inline" v-if="pet.shy === true">, </p>
          <p style="display:inline" v-if="pet.shy === true" >Shy</p>
          <p style="display:inline" v-if="pet.mischievous === true">, </p>
          <p style="display:inline" v-if="pet.mischievous === true" >Mischievous</p>
          <p style="display:inline" v-if="pet.independent === true">, </p>
          <p style="display:inline" v-if="pet.independent === true" >Independent</p>
          <p></p>
          <p>Other comments: "{{ pet.otherComments }}"</p>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-btn
            text
            color="teal accent-4"
            @click="reveal = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
       <v-card
        v-if="revealRequestedPet"
        class="transition-fast-in-fast-out v-card--revealRequestedPet"
        style="height: 100%;"
      >

        <v-card-text class="pb-0">
          <p class="text-h4 text--primary">
            {{ requestedPet.petName }}
          </p>

        <v-img
         height="250"
          :src="this.requestedPet.petImage"
        ></v-img>
            
          <p> {{requestedPet.species}}, {{requestedPet.breed}}, {{requestedPet.age}} years old</p>
          <p style="display:inline" >Attributes: </p>
          <p style="display:inline" v-if="requestedPet.playful === true" >Playful</p>
          <p style="display:inline" v-if="requestedPet.nervous === true & requestedPet.playful === true">, </p>
          <p style="display:inline" v-if="requestedPet.nervous === true" >Nervous</p>
          <p style="display:inline" v-if="requestedPet.confident === true">, </p>
          <p style="display:inline" v-if="requestedPet.confident === true" >Confident</p>
          <p style="display:inline" v-if="requestedPet.shy === true">, </p>
          <p style="display:inline" v-if="requestedPet.shy === true" >Shy</p>
          <p style="display:inline" v-if="requestedPet.mischievous === true">, </p>
          <p style="display:inline" v-if="requestedPet.mischievous === true" >Mischievous</p>
          <p style="display:inline" v-if="requestedPet.independent === true">, </p>
          <p style="display:inline" v-if="requestedPet.independent === true" >Independent</p>
          <p></p>
          <p>Other comments: "{{ requestedPet.otherComments }}"</p>
        </v-card-text>
        <v-card-actions class="pt-0">
          <v-btn
            text
            color="teal accent-4"
            @click="revealRequestedPet = false"
          >
            Close
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-expand-transition>
    
  </v-card>
</template>

<script>
import playdateService from "@/services/PlaydateService";
import petService from "@/services/PetService";
import userService from '@/services/UserService';
export default {
  name: "playdate-card",
  props: ["playdate"],
  data() {
    return {
      reveal: false,
      revealRequestedPet: false,
      pet: {},
      requestedPet: {},
      user:{}
    };
  },
  methods: {
    getPetInfo() {
      petService.getPet(this.playdate.playdatePostedPetId).then((response) => {
       
          this.pet = response.data;
        })
      
    },
    getRequestedPetInfo() {
      petService.getPet(this.playdate.playdateRequestedPetId).then((response) => {
       
          this.requestedPet = response.data;
        })

    },
     getUserInfo() {
      userService.displayUser(this.playdate.playdatePostedUserId).then((response) => {
       
          this.user = response.data;
        })
     },
      deletePlaydate() {
        if (
          confirm(
            "Are you sure you want to delete this playdate? This action cannot be undone."
          )
        ) {
          playdateService
            .deleteLoggedInUserPlaydate(this.playdate.playdateId)
            .then((response) => {
              if (response.status === 200) {
                alert("Playdate successfully deleted.");
                this.$router.push(`/`);
              }
            })
            .catch((error) => {
              if (error.response) {
                this.errorMsg =
                  "Error deleting playdate. Response received was '" +
                  error.response.statusText +
                  "'.";
              } else if (error.request) {
                this.errorMsg =
                  "Error deleting playdate. Server could not be reached.";
              } else {
                this.errorMsg =
                  "Error deleting playdate. Request could not be created.";
              }
            });
        }
      },
      acceptPlaydate() {
        const updatedPlaydate = {
            playdateId: this.playdateId,
            playdateStatusId: this.playdateStatusId
          };
        if (
          confirm(
            "Are you sure you want to accept this playdate?"
          )
        ) {
          playdateService
            .acceptPlaydate(updatedPlaydate, this.playdate.playdateId)
            .then((response) => {
              if (response.status === 200) {
                alert("Playdate successfully approved.");
                this.$router.push(`/`);
              }
            })
            .catch((error) => {
              if (error.response) {
                this.errorMsg =
                  "Error accepting playdate. Response received was '" +
                  error.response.statusText +
                  "'.";
              } else if (error.request) {
                this.errorMsg =
                  "Error accepting playdate. Server could not be reached.";
              } else {
                this.errorMsg =
                  "Error accepting playdate. Request could not be created.";
              }
            });
        }
      },
      rejectPlaydate() {
        const updatedPlaydate = {
            playdateId: this.playdateId,
            playdateStatusId: this.playdateStatusId
          };
        if (
          confirm(
            "Are you sure you want to reject this playdate?"
          )
        ) {
          playdateService
            .rejectPlaydate(updatedPlaydate, this.playdate.playdateId)
            .then((response) => {
              if (response.status === 200) {
                alert("Playdate successfully rejected.");
                this.$router.push(`/`);
              }
            })
            .catch((error) => {
              if (error.response) {
                this.errorMsg =
                  "Error rejecting playdate. Response received was '" +
                  error.response.statusText +
                  "'.";
              } else if (error.request) {
                this.errorMsg =
                  "Error rejecting playdate. Server could not be reached.";
              } else {
                this.errorMsg =
                  "Error rejecting playdate. Request could not be created.";
              }
            });
        }
      }          
  },
  created() {
      this.getPetInfo();
      this.getRequestedPetInfo();
      this.getUserInfo();
  },
};
</script>

<style>
.v-card--reveal {
  bottom: 0;
  opacity: 1 !important;
  position: absolute;
  width: 100%;
}

.v-card--revealRequestedPet {
  bottom: 0;
  opacity: 1 !important;
  position: absolute;
  width: 100%;
}

</style>