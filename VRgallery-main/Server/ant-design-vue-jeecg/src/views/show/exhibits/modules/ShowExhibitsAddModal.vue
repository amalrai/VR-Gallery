<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    switchFullscreen
    @ok="handleOk"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    :cancelText="$t('关闭')"
    :okText="$t('确定')">
    <show-exhibits-add-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-exhibits-add-form>
  </j-modal>
</template>

<script>

  import ShowExhibitsAddForm from './ShowExhibitsAddForm'
  export default {
    name: 'ShowExhibitsAddModal',
    components: {
      ShowExhibitsAddForm
    },
    data () {
      return {
        title:'',
        width:800,
        visible: false,
        disableSubmit: false,
        roomId:''
      }
    },
    methods: {
      add (roomId) {
        this.visible=true
        this.$nextTick(()=>{
          this.roomId = roomId;
          this.$refs.realForm.add(roomId);
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleOk () {
        this.$refs.realForm.submitForm();
      },
      submitCallback(){
        this.$emit('ok');
        this.visible = false;
      },
      handleCancel () {
        this.close()
      }
    }
  }
</script>