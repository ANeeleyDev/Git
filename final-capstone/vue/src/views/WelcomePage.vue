<template>
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
  <v-carousel hide-delimiters>
    <v-carousel-item
      v-for="pet in this.pets"
      :key="pet.petId"
      
    >
    <pet-card v-bind:pet = "pet" />
    </v-carousel-item>
  </v-carousel>

    
  </v-container>
</template>

<script>

import PetCard from '../components/PetCard.vue';
import petService from '@/services/PetService';

export default {
  name: "welcome-page",
  components: { 
    PetCard,    
  },  
  methods: {
        getTop5Pets(){            
            petService.getTopFivePets()
            .then(response => {
          this.pets = response.data;          
        })
        },
    },
     created(){
        this.getTop5Pets();
    },
  data(){
    return {
      pets: {},      
      }}
};
</script>

<style>
</style>