<template>
  <div>
    <h1>Add Playdate</h1>
    <playdate-form v-bind:playdateId="this.playdateId" />   
    <v-form v-on:submit.prevent="submitForm">
      <v-container fluid>
        <v-row align="center">
          <v-col cols="1">
            <v-subheader class="h3 mb-3 font-weight-normal"></v-subheader>
          </v-col>

          <v-col cols="6">
            <v-select
              class="selector"
              v-model="playdate.playdatePostedPetId"
              :items="pets"
              item-text="petName"
              item-value="petId"
              label="Select Pet"
              single-line
            ></v-select>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="4">
            <v-text-field
              v-model="playdate.playdateAddress"
              label="Address"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="4">
            <v-select
              :items="city"
              label="City"
              v-model="playdate.playdateCity"
            ></v-select>
          </v-col>
          <v-col cols="12" md="4">
            <!-- <v-autocomplete
              v-model="playdate.playdateState"
              :loading="loading"
              :items="state"
              :search-input.sync="search"
              cache-items
              class="mx-4"
              flat
              hide-no-data
              hide-details
              label="What state are you from?"
              solo-inverted
            ></v-autocomplete> -->
            <v-select
              :items="state"
              label="State"
              v-model="playdate.playdateState"
            ></v-select>
          </v-col>
          <v-col cols="12" md="4">
            <v-select
              :items="zip"
              label="Zip Code"
              v-model="playdate.playdateZip"
            ></v-select>
          </v-col>
        </v-row>
      </v-container>

      <!-- <v-text-field
        value="userFirstName"
        label="Owner First Name"
        v-model="user.firstName"
      ></v-text-field> -->

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
//import userService from '@/services/UserService';
import playdateService from "@/services/PlaydateService";
export default {
  name: "create-playdate-form",
  props: ["petId"],
  data() {
    return {
      select: [],
      cities:[],

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
      city: [
        {
          text: "Cincinnati",
          value: "184",
        },
        {
          text: "Columbus",
          value: "207",
        },
        {
          text: "Toledo",
          value: "947",
        },
        {
          text: "Cleveland",
          value: "191",
        },
        {
          text: "Dayton",
          value: "238",
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
  },

  methods: {
    getUserPets() {
      petService.getPetList().then((response) => {
        this.pets = response.data;
      });
    },
    submitForm() {
      this.playdate.playdateRequestedPetId = this.playdate.playdatePostedPetId;
      this.playdate.meetingTime = this.submittableDateTime;
      playdateService.registerPlaydate(this.playdate).then((response) => {
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