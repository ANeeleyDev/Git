<template>
  <div>
    <div class="pet-info">
      <h2>{{ pet.petName }}</h2>
      <p>{{ pet.age }} year old {{ pet.species }} {{ pet.breed }}</p>
      <p>{{ pet.otherComments }}</p>
      <button @click="$router.push({name: 'edit-pet', params: {petId: pet.petId}})" >Edit Pet</button>
      <button @click="deletePet">Delete Pet</button>
    </div>
  </div>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "pet-detail",
  props: ["pet"],
  methods: {
    deletePet() {
      if (
        confirm(
          "Are you sure you want to delete this pet? This action cannot be undone."
        )
      ) {
        petService
          .deletePet(this.pet.petId)
          .then((response) => {
            if (response.status === 200) {
              alert("Pet successfully deleted");
              this.$router.push(`/`);
            }
          })
          .catch((error) => {
            if (error.response) {
              this.errorMsg =
                "Error deleting pet. Response received was '" +
                error.response.statusText +
                "'.";
            } else if (error.request) {
              this.errorMsg =
                "Error deleting pet. Server could not be reached.";
            } else {
              this.errorMsg =
                "Error deleting pet. Request could not be created.";
            }
          });
      }
    },    
  },
};
</script>

<style>
.pet-info {
  font-family: arial;
  padding: 5px;
  border: 5px solid blue;
  background-color: grey;
}
</style>