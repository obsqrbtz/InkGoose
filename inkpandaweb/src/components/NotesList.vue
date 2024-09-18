<template>
    <button @click="createNote" class="btn btn-neutral m-4">Create note</button>
    <div v-if="data" class="grid grid-cols-3">
        <div v-for="item in data" class="card bg-base-100 w-96 shadow-md m-4">
            <div class="card-body">
                <h2 class="card-title">
                    {{ item.title }}
                </h2>
                <p> {{ item.content }} </p>
                <div class="grid grid-cols-2 h-8">
                    <button class="btn btn-outline btn-default mx-2">Edit</button>
                    <button class="btn btn-outline btn-default mx-2">Delete</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        created() {
            this.fetchNotes();
        },
        data() {
            return {
                data: null,
            };
        },
        methods: {
            async fetchNotes() {
                const response = await fetch("https://localhost:7193/api/Notes/GetNotesList");
                this.data = await response.json();
            },
            async createNote() {
                const response = await fetch("https://localhost:7193/api/Notes/AddNote?title=New%20note&content=Note%20was%20created%20successfully", {
                    method: "POST",
                    headers: {
                        "Content-type": "application/json; charset=UTF-8"
                    }
                });
            }
        }
    };
</script>