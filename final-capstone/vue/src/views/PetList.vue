<template>
  <div>
      <div v-for="pet in pets" v-bind:key="pet.petId">
          <pet-detail v-bind:pet = "pet" />
      </div>
  </div>
</template>

<script>
import petService from '@/services/PetService';
import PetDetail from '../components/PetDetail.vue';
export default {
  components: { PetDetail },
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
        }
    },
    created(){
        this.getUserPets();
    }

}
</script>

<style>

</style>