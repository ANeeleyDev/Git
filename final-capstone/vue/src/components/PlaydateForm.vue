<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-select
        v-model="playdate.playdatePostedPetId"
        :hint="`${select.petName}`"
        :items="pets"
        item-text="petName"
        item-value="petId"
        label="Select Pet"
        persistent-hint
        single-line
      ></v-select>
      <v-text-field
        value="playdateAddress"
        label="Address"
        v-model="playdate.playdateAddress"
      ></v-text-field>
     <v-autocomplete :items="city" label="City" item-text="cityName" item-value="cityId" v-model="playdate.playdateCity"></v-autocomplete>
      <v-select
        :items="state"
        label="State"
        v-model="playdate.playdateState"
      ></v-select>
      <v-select
        :items="zip"
        label="Zip"
        v-model="playdate.playdateZip"
      ></v-select>
      <v-date-picker v-model="date"></v-date-picker>
      <br />
      <v-time-picker v-model="time" format="ampm"></v-time-picker>
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
import userService from "../services/UserService";

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
        playdateRequestedUserId: "",
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
      city: [],
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
  computed: {
    submittableDateTime() {
      const date = new Date(this.date);
      if (typeof this.time === "string") {
        const hours = this.time.match(/^(\d+)/)[1];
        const minutes = this.time.match(/:(\d+)/)[1];
        date.setHours(hours);
        date.setMinutes(minutes);
      } else {
        date.setHours(this.time.getHours());
        date.setMinutes(this.time.getMinutes());
      }
      return date;
    },
  },
  created() {
    this.getUserPets();
    this.getAllCities();
    if (this.playdateId != 0) {
      petService
        .getPlaydateForDisplay(this.playdateId)
        .then((response) => {
          this.playdate = response.data;
        })
        .catch((error) => {
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
    getAllCities() {
      userService.getAllCities().then((response) => {
        if (response.status === 200) {
          this.city = response.data;
        }
      });
    },
    submitForm() {
      const newPlaydate = {
        playdatePostedUserId: Number(this.$store.state.user.userId),
        playdatePostedPetId: this.playdate.playdatePostedPetId,
        meetingTime: this.submittableDateTime,
        playdateAddress: this.playdate.playdateAddress,
        playdateCity: String(this.playdate.playdateCity),
        playdateState: this.playdate.playdateState,
        playdateZip: this.playdate.playdateZip,
        playdateStatusId: this.playdateStatusId,
      };

      if (this.editPlaydateId === undefined) {
        //add here
        playdateService
          .registerPlaydate(newPlaydate)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/userPlaydateView`);
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
      this.$router.push(`/userPlaydateView`);
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
        this.errorMsg =
          "Error " + verb + " playdate. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " playdate. Request could not be created.";
      }
    },
  },
};
</script>

<style>
</style>