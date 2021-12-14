<template>
  <div>
      
    <v-row>
      <v-col v-for="pet in pets" v-bind:key="pet.petId">
          <pet-card v-bind:pet = "pet" />
      </v-col>
      </v-row>
  </div>
</template>

<script>
import petService from '@/services/PetService';
import PetCard from '../components/PetCard.vue';
export default {
  components: { PetCard },
    name: 'petList',
   
    data(){
        return {
            pets: {},
            currentUserId: this.$store.state.user.userId,
        }
    },
    methods: {
        getUserPets(){            
            petService.getPetList(this.currentUserId)
            .then(response => {
          this.pets = response.data;          
        })
        },
        
    },
    created(){
        this.getUserPets();
    }

}
</script>

<style>

</style>