<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-text-field
        value="playdateAddress"
        label="Address"        
        v-model.number="playdate.playdateAddress"
      ></v-text-field>
      <v-select 
        :items="city"
        label="City"
        v-model="playdate.city"
      ></v-select>
      <v-select 
        :items="state"
        label="State"
        v-model="playdate.state"
      ></v-select>
      <v-select 
        :items="zip"
        label="Zip"
        v-model="playdate.Zip"
      ></v-select>
      <v-date-picker
        v-model="date"
      ></v-date-picker>
    <br />
      <v-time-picker
        v-model="time" format="ampm"
      ></v-time-picker>
    <br />
    <br />
      <v-btn type="submit"> Submit </v-btn>
      <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </v-btn>
    </v-form>    
    
  </div>
</template>

<script>
import petService from "@/services/PetService";
import playdateService from "../services/PlaydateService";

export default {
    name: "playdate-form",
    props: ["playdateId"],

    data() {
        return {
            select: [],
            pets: [],
            playdate: {
                playdatePostedUserId: this.$store.state.user.userId,
                playdatePostedPetId: "",
                playdateRequestedUserId: this.$store.state.user.userId,
                playdateRequestedPetId: "",
                meetingTime: "",
                playdateAddress: "",
                playdateCity: "",
                playdateState: "",
                playdateZip: "",
                playdateStatusId: 0,
            },
            currentUserId: this.$store.state.user.userId,
            date: this.submittableDateTime,
            time: this.submittableDateTime,
            city: [
                {
                text: "Cincinnati",
                value: "0",
                },
                {
                text: "Columbus",
                value: "1",
                },
                {
                text: "Toledo",
                value: "2",
                },
                {
                text: "Cleveland",
                value: "3",
                },
                {
                text: "Dayton",
                value: "4",
                },
            ],
            state: [
                {
                text: "Ohio",
                value: "34",
                },
            ],
            zip: [
                {
                text: "45249",
                value: "0",
                },
                {
                text: "41073",
                value: "1",
                },
                {
                text: "45201",
                value: "2",
                },
                {
                text: "45222",
                value: "3",
                },
                {
                text: "45237",
                value: "4",
                },
            ],
            editPlaydateId: this.$route.params.playdateId,
        };
    }, 
    created() {
    if (this.playdateId != 0) {
      petService
        .getPlaydateForDisplay(this.playdateId)
        .then(response => {
          this.playdate = response.data;
        })
        .catch(error => {
          if (error.response && error.response.status === 404) {
            alert(
              "Card not available. This playdate may have been deleted or you have entered an invalid playdate ID."
            );
            this.$router.push("/");
          }
        });
    }
  },
    methods: {
        submitForm() {
        const newPlaydate = {
            playdatePostedUserId: Number(this.$store.state.user.userId),
            playdatePostedPetId: this.playdate.playdatePostedPetId,
            playdateRequestedUserId: this.playdate.playdateRequestedUserId,
            playdateRequestedPetId: this.playdate.playdateRequestedPetId,
            meetingTime: this.playdate.meetingTime,
            playdateAddress: this.playdate.playdateAddress,
            playdateCity: this.playdateCity,
            playdateState: this.playdateState,
            playdateZip: this.playdateZip,
            playdateStatusId: this.playdateStatusId
        };

        if (this.editPlaydateId === undefined) {
        //add here
        playdateService
          .registerPlaydate(newPlaydate)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/playdatelist`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update else

        newPlaydate.playdateId = this.editPlaydateId;

        playdateService

          .updateLoggedInUserPlaydate(newPlaydate, this.editPlaydateId)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/UserPlaydateView`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    cancelForm() {
      this.$router.push(`/playdatelist`);
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

<style>
</style>