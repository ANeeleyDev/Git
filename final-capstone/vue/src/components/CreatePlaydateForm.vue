<template>
  <div>
    <h1>Add Playdate</h1>
    <playdate-form v-bind:playdateId="this.playdateId" />
    <v-form v-on:submit.prevent="submitForm">
      <v-select
        v-model.="playdate.playdatePostedPetId"
        :items="pets"
        item-text="petName"
        item-value="petId"
        label="Select Pet"
        single-line
      ></v-select>

      <v-text-field
        v-model="playdate.playdateAddress"
        label="Address"
        required
      ></v-text-field>

      <v-autocomplete
        :items="city"
        name="city"
        item-text="cityName"
        item-value="cityId"
        label="City"
        v-model="playdate.playdateCity"
      ></v-autocomplete>

      <v-select
        :items="state"
        label="State"
        v-model="playdate.playdateState"
      ></v-select>

      <v-select
        :items="zip"
        label="Zip Code"
        v-model="playdate.playdateZip"
      ></v-select>

      <section class="time-date">
        <v-layout row>
          <h4>Choose a Date and Time</h4>
        </v-layout>

        <v-layout row class="date-picker">
          <v-date-picker v-model="date"></v-date-picker>
          <p>{{ date }}</p>
        </v-layout>
        <v-spacer></v-spacer>

        <v-layout row class="time-picker">
          <v-time-picker v-model="time" format="ampm"></v-time-picker>
          <p>{{ time }}</p>
        </v-layout>
      </section>

      <v-btn type="submit"> Submit </v-btn>
      <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import petService from "@/services/PetService";
import userService from "@/services/UserService";
import playdateService from "@/services/PlaydateService";
export default {
  name: "create-playdate-form",
  props: ["petId"],
  data() {
    return {
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
      //petName: this.pet.petName,
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
  },

  methods: {
    getAllCities() {
      userService.getAllCities().then((response) => {
        if (response.status === 200) {
          this.city = response.data;
        }
      });
    },
    getUserPets() {
      petService.getPetList().then((response) => {
        this.pets = response.data;
      });
    },
    submitForm() {
      const newPlaydate = {
        playdatePostedUserId: Number(this.$store.state.user.userId),
        playdatePostedPetId: Number(this.playdate.playdatePostedPetId),
        meetingTime: this.submittableDateTime,
        playdateAddress: this.playdate.playdateAddress,
        playdateCity: String(this.playdate.playdateCity),
        playdateState: this.playdate.playdateState,
        playdateZip: this.playdate.playdateZip,
        playdateStatusId: this.playdateStatusId,
      };
      playdateService.registerPlaydate(newPlaydate).then((response) => {
        this.pets = response.data;
        this.$router.push(`/UserPlaydateView`);
      });
    },

    cancelForm() {
      this.$router.push(`/UserPlaydateView`);
    },

    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " pet. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg = "Error " + verb + " pet. Server could not be reached.";
      } else {
        this.errorMsg = "Error " + verb + " pet. Request could not be created.";
      }
    },
  },
};
</script>

<style>
.time-date {
  margin-left: 95px;
  padding-top: 40px;
}

.selector {
  width: 100%;
  padding: 16px 20px;
  margin-top: 50px;
  border: none;
  border-radius: 4px;
  background-color: #f1f1f1;
}
</style>