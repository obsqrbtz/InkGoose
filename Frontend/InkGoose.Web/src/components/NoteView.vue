<script setup>
import markdownit from 'markdown-it'
import { defineComponent } from 'vue';
const markdown = markdownit({
    html: true,
    linkify: true,
    typographer: true
});
const emit = defineEmits(['titleUpdated', 'contentUpdated'])
</script>
<template>
    <div class="flex w-full flex-col lg:flex-row h-full bg-base-100  base-content ">
        <div class="w-full">
            <div class="p-2">
                <input
                    v-model="editTitle"
                    type="text"
                    placeholder="Title"
                    class="input w-full bg-base-100   focus:border-none focus:outline-none font-bold"
                    @input="$emit('titleUpdated', editTitle)"
                >
            </div>
            <div class="h-3/4">
                <textarea
                    v-model="editContent"
                    class="pl-6 bg-base-100  leading-snug font-mono textarea textarea-ghost textarea-md h-full whitespace-break-spaces w-full focus:border-none focus:outline-none"
                    placeholder="Content"
                    @input="$emit('contentUpdated', editContent)"
                />
            </div>
        </div>
        <div class="divider lg:divider-horizontal" />
        <div class="w-full">
            <div class="">
                <div class="text-left prose prose-md ml-2 mb-8">
                    <h1> {{ editTitle }} </h1>
                </div>
                <div class=" h-3/4">
                    <div
                        class="prose prose-md text-left m-2"
                        v-html="markdown.render(editContent)"
                    />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default defineComponent({
    props: {
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
    },
    data() {
        return {
            editTitle: this.title,
            editContent: this.noteContent
        }
    },
})
</script>