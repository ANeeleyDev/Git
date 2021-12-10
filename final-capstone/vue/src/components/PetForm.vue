<template>
  <div>
    <v-form v-on:submit.prevent="submitForm">
      <v-text-field
        value="petName"
        label="Name"
        v-model="pet.petName"
      ></v-text-field>
       <v-text-field
        value="petImage"
        label="Pet Image URL"        
        v-model.number="pet.petImage"
      ></v-text-field>
      <v-text-field
        value="age"
        label="Age"        
        v-model.number="pet.age"
      ></v-text-field>
      <v-select
        :items="species"
        label="Species"
        v-model="pet.species"
      ></v-select>
      <v-select :items="breed" label="Breed" v-model="pet.breed"></v-select>
      <v-textarea
        name="input-7-1"
        label="Other Comments"
        value="What else should we know?."
        v-model="pet.otherComments"
      ></v-textarea>

      <v-checkbox
        label="Playful"        
        v-model="pet.playful"
      ></v-checkbox>
      <v-checkbox
        v-model="pet.mischievous"
        label="Mischievous"
        
      ></v-checkbox>
      <v-checkbox v-model="pet.shy" label="Shy"></v-checkbox>
      <v-checkbox v-model="pet.nervous" label="Nervous"></v-checkbox>
      <v-checkbox v-model="pet.confident" label="Confident"></v-checkbox>
      <v-checkbox v-model="pet.independent" label="Independent"></v-checkbox>

      <v-btn type="submit"> Submit </v-btn>
      <v-btn class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </v-btn>
    </v-form>    
    <!-- <form v-on:submit.prevent="submitForm">
      <div>
        <label for="petName">Name:</label>
        <input id="petName" type="text" class="" v-model="pet.petName" />
      </div>
      <div>
        <label for="age">Age:</label>
        <input id="age" type="number" class="" v-model="pet.age" />
      </div>
      <div>
        <label for="species">Species:</label>
        <input id="species" type="text" class="" v-model="pet.species" />
      </div>
      <div>
        <label for="breed">Breed:</label>
        <input id="breed" type="text" class="" v-model="pet.breed" />
      </div>
      <div>
        <label for="otherComments">Other Comments:</label>
        <br />
        <textarea
          id="otherComments"
          name="otherComments"
          rows="4"
          cols="50"
          v-model="pet.otherComments"
        >
  What else should we know about your pet?
  </textarea
        >
        >
      </div>
      <div>
        <p>Personality:</p>

        <div>
          <input
            type="checkbox"
            id="playful"
            name="playful"
            v-model="pet.playful"
            checked
          />
          <label for="playful">Playful</label>
        </div>

        <div>
          <input
            type="checkbox"
            id="nervous"
            name="nervous"
            v-model="pet.nervous"
          />
          <label for="nervous">Nervous</label>
        </div>
        <div>
          <input
            type="checkbox"
            id="confident"
            name="confident"
            v-model="pet.confident"
          />
          <label for="confident">Confident</label>
        </div>
        <div>
          <input type="checkbox" id="shy" name="shy" v-model="pet.shy" />
          <label for="shy">Shy</label>
        </div>
        <div>
          <input
            type="checkbox"
            id="mischievous"
            name="mischievous"
            v-model="pet.mischievous"
          />
          <label for="mischievous">Mischievous</label>
        </div>
        <div>
          <input
            type="checkbox"
            id="independent"
            name="independent"
            v-model="pet.independent"
          />
          <label for="independent">Independent</label>
        </div>
      </div>
      <button class="">Submit</button>
      <button class="" v-on:click.prevent="cancelForm" type="cancel">
        Cancel
      </button>
    </form> -->
  </div>
</template>

<script>
import petService from "@/services/PetService";
export default {
  name: "pet-form",
  props: ["petId"],

  data() {
    return {
      species: [
        {
          text: "Dog",
          value: "0",
        },
        {
          text: "Cat",
          value: "1",
        },
      ],
      breed: [
        {
          text: "Holder",
          value: "0",
        },
      ],
      pet: {
        petId: "",
        petName: "",
        petImage: "",
        age: "",
        species: "",
        breed: "",
        otherComments: "",
        playful: false,
        nervous: false,
        confident: false,
        shy: false,
        mischievous: false,
        independent: false,
      },
      editPetId: this.$route.params.petId,
    };
  },
  created() {
    if (this.$route.params.petId != undefined) {
      petService
        .getPet(this.$route.params.petId)
        .then((response) => {
          this.pet = response.data;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Pet not available. This pet may have been deleted or you have entered an invalid pet ID."
            );
            this.$router.push("/");
          }
        });
    }
  },
  methods: {
    submitForm() {
      const newPet = {
        //userId: Number(this.$store.state.user.userId),
        petName: this.pet.petName,
        petImage: this.pet.petImage,
        age: this.pet.age,
        species: this.pet.species,
        breed: this.pet.breed,
        otherComments: this.pet.otherComments,
        playful: this.pet.playful,
        nervous: this.pet.nervous,
        confident: this.pet.confident,
        shy: this.pet.shy,
        mischievous: this.pet.mischievous,
        independent: this.pet.independent,
      };

      if (this.editPetId === undefined) {
        //add here
        petService
          .addPet(newPet)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/petlist`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update else

        newPet.petId = this.editPetId;

        petService

          .updatePet(this.editPetId, newPet)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/petlist`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
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