<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-container fluid>
        <v-row align="center">
          <v-col cols="1">
            <v-subheader class="h3 mb-3 font-weight-normal"></v-subheader>
          </v-col>

          <v-col cols="6">
            <v-select class="selector"
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
export default {
  name: "create-playdate-form",
  props: ["petId"],
  data() {
    return {
      select:{},
      items:{},
      pets: {},
      currentUserId: this.$store.state.user.userId,
      //petName: this.pet.petName,
      date: this.submittableDateTime,
      time: this.submittableDateTime,
      
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
      })
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
.time-date{
margin-left:95px;
padding-top: 40px
  
}

.selector{
  width: 100%;
  padding: 16px 20px;
  margin-top: 50px;
  border: none;
  border-radius: 4px;
  background-color: #f1f1f1;
}

</style>