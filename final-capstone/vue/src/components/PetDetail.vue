<template>
  <div>
    <div class="pet-info">
    <h2>{{pet.petName}}</h2>
    <p>{{pet.age}} year old {{pet.species}} {{pet.breed}}</p>
    <p>{{pet.otherComments}}</p>
    
  </div>
  </div>
</template>

<script>
export default {
    name: 'pet-detail',
    props: ['pet']
    }


</script>

<style>
.pet-info{
  font-family: arial;
  padding: 5px;
  border: 5px solid blue;
  background-color: grey;
}

</style>