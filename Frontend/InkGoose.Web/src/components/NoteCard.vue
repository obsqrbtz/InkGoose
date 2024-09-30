<script setup>
const props = defineProps({
    id: {
        type: String,
        required: true
    },
    title: {
        type: String,
        required: true
    },
    noteContent: {
        type: String,
        required: true
    }
});

</script>

<template>
    <div class="card bg-base-100 w-96 shadow-md m-4">
        <div class="card-body">
            <h2 class="card-title">
                {{ title }}
            </h2>
            <p> {{ noteContent }} </p>
            <div class="grid grid-cols-2 h-8">
                <button @click="showModal = true" class="btn btn-outline btn-default mx-2">Edit</button>
                <NoteEdit :isOpen="showModal" @update:isOpen="showModal = $event" :id="id" :title="title"
                    :noteContent="noteContent" />
                <button @click="deleteNote(id)" class="btn btn-outline btn-default mx-2">Delete</button>
            </div>
        </div>
    </div>
</template>

<script>
import NoteEdit from './NoteEdit.vue'
import { ref } from 'vue';
const components = {
    NoteEdit,
}
export default {
    components: {
        NoteEdit
    },
    data() {
        return {
            showModal: false
        };
    },
    methods: {
        async deleteNote(id) {
            const response = await fetch(`${this.apiHost}/DeleteNote?id=${id}`, {
                method: "DELETE",
                headers: {
                    "Content-type": "application/json; charset=UTF-8"
                },
            });
        }
    },
}
</script>