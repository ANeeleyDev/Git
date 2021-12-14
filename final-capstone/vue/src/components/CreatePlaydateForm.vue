<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-container fluid>
        <v-row align="center">
          <v-col cols="6">
            <v-subheader> Select Pet </v-subheader>
          </v-col>

          <v-col cols="6">
            <v-select
              v-model="select"
              :hint="`${select.petName}`"
              :items="pets"
              item-text="petName"
              item-value="petId"
              label="Select Pet"
              persistent-hint
              return-object
              single-line
            ></v-select>
          </v-col>
        </v-row>
      </v-container>

      <!-- <v-text-field
        value="userFirstName"
        label="Owner First Name"
        v-model="user.firstName"
      ></v-text-field> -->

      <v-layout row>
        <v-flex xs 12 sm6 offset-sm3>
          <h4>Choose a Date and Time</h4>
        </v-flex>
      </v-layout>

      <v-layout row class="date-picker">
        <v-flex xs 12 sm6 offset-sm3>
          <v-date-picker v-model="date"></v-date-picker>
          <p>{{ date }}</p>
        </v-flex>
      </v-layout>

      <v-layout row class="time-picker">
        <v-flex xs 12 sm6 offset-sm3>
          <v-time-picker v-model="time" format="ampm"></v-time-picker>
          <p>{{ time }}</p>
        </v-flex>
      </v-layout>

      <v-btn type="submit"> Submit </v-btn>
      <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </v-btn>
    </v-form>
  </div>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "create-playdate-form",
  props: ["petId", "userId"],
  data() {
    return {
      select:{},
      items:{},
      pets: {},
      currentUserId: this.$store.state.user.userId,
      //petName: this.pet.petName,
      date: new Date(),
      time: new Date(),
      
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

    cancelForm() {
      this.$router.push(`/petlist`);
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
</style>