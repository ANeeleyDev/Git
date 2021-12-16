<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
    <v-select
        v-model="playdate.playdateRequestedPetId"
        :items="pets"
        item-text="petName"
        item-value="petId"
        label="Select Pet"
        single-line
    ></v-select>
    <v-btn type="submit"> Submit </v-btn>
    <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
    </v-btn>
    </v-form>    
    
  </div>
</template>

<script>
import petService from "@/services/PetService";
import playdateService from "@/services/PlaydateService";

export default {
    name: "request-playdate-form",
    props: ["playdateId"],

     data() {
        return {
            select: [],
            pets: [],
            playdate: {
                playdatePostedUserId: "",
                playdatePostedPetId: "",
                playdateRequestedUserId: this.$store.state.user.userId,
                playdateRequestedPetId: "",
                meetingTime: "",
                playdateAddress: "",
                playdateCity: "",
                playdateState: "",
                playdateZip: "",
                playdateStatusId: "",           
                },
            currentUserId: this.$store.state.user.userId,
            editPlaydateId: this.$route.params.playdateId
        };
     },
    created() {
        this.getUserPets();
        if (this.playdateId != 0) {
        petService
            .getPlaydateForDisplay(this.playdateId)
            .then(response => {
            this.playdate = response.data;
            })
            .catch(error => {
            if (error.response && error.response.status === 404) {
                alert(
                "Playdate not available. This playdate may have been deleted or you have entered an invalid playdate ID."
                );
                this.$router.push("/");
          }
        });
    }
  },  
    methods: {
        getUserPets() {
            petService.getPetList().then((response) => {
            this.pets = response.data;
        });
    },
        submitForm() {
            const newPlaydate = {
            playdateRequestedUserId: Number(this.$store.state.user.userId),
            playdateRequestedPetId: this.playdate.playdateRequestedPetId,
            playdateStatusId: this.playdateStatusId
        };
        if (this.editPlaydateId === undefined) {
        //add here
        playdateService
          .registerPlaydate(newPlaydate)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/userPlaydateViewInProgress`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update else

        newPlaydate.playdateId = this.editPlaydateId;

        playdateService

          .requestPlaydate(newPlaydate, this.editPlaydateId)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/UserPlaydateViewInProgress`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
        cancelForm() {
        this.$router.push(`/userPlaydateViewInProgress`);
    },
        handleErrorResponse(error, verb) {
        if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " playdate. Response received was '" +
          error.response.statusText +
          "'.";
        } else if (error.request) {
        this.errorMsg = "Error " + verb + " playdate. Server could not be reached.";
        } else {
        this.errorMsg = "Error " + verb + " playdate. Request could not be created.";
        }
    },
    },

};

</script>

