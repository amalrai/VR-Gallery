<template>
  <a-card :bordered="false">
    <!-- 查询区域 -->
    <div class="table-page-search-wrapper">
      <a-form layout="inline" @keyup.enter.native="searchQuery">
        <a-row :gutter="24">

          <a-col :md="6" :sm="8">
            <a-form-item :label="$t('账号')">
              <a-input :placeholder="$t('请输入账号查询')" v-model="queryParam.showUserEmail"></a-input>
            </a-form-item>
          </a-col>

          <a-col :md="6" :sm="8">
            <a-form-item :label="$t('展馆')">
              <j-search-select-tag
                ref="selectTag"
                :placeholder="$t('请选择展馆')"
                v-model="queryParam.showMuseumId"
                dict="show_museum,name,id"
                :pageSize="6"
                :async="true">
              </j-search-select-tag>
            </a-form-item>
          </a-col>

          <a-col :md="12" :sm="16">
            <a-form-item :label="$t('付费时间')">
              <j-date :placeholder="$t('请选择开始时间')" class="query-group-cust" v-model="queryParam.payTime_begin" show-time date-format="YYYY-MM-DD HH:mm:ss"></j-date>
              <span class="query-group-split-cust"></span>
              <j-date :placeholder="$t('请选择结束时间')" class="query-group-cust" v-model="queryParam.payTime_end" show-time date-format="YYYY-MM-DD HH:mm:ss"></j-date>
            </a-form-item>
          </a-col>

          <a-col :md="6" :sm="8">
            <a-form-item :label="$t('状态')">
              <a-select :placeholder="$t('选择状态查询')" v-model="queryParam.status" >
                <a-select-option value="">{{$t('请选择')}}</a-select-option>
                <a-select-option value="0">{{$t('未支付')}}</a-select-option>
                <a-select-option value="1">{{$t('已支付')}}</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>

          <a-col :md="6" :sm="8">
                      <span style="float: left;overflow: hidden;" class="table-page-search-submitButtons">
                        <a-button type="primary" @click="searchQuery" icon="search">{{$t('查询')}}</a-button>
                        <a-button type="primary" @click="searchReset" icon="reload" style="margin-left: 8px">{{$t('重置')}}</a-button>
                      </span>
          </a-col>
        </a-row>
      </a-form>
    </div>
    <!-- 查询区域-END -->

    <!-- 操作按钮区域 -->
    <div class="table-operator">
      <a-button type="primary" icon="download" @click="handleExportXls($t('订单管理'))">{{$t('导出')}}</a-button>
    </div>

    <!-- table区域-begin -->
    <div>
<!--      <div class="ant-alert ant-alert-info" style="margin-bottom: 16px;">-->
<!--        <i class="anticon anticon-info-circle ant-alert-icon"></i> 已选择 <a style="font-weight: 600">{{ selectedRowKeys.length }}</a>项-->
<!--        <a style="margin-left: 24px" @click="onClearSelected">清空</a>-->
<!--      </div>-->

      <a-table
        ref="table"
        size="middle"
        :scroll="{x:true}"
        bordered
        rowKey="id"
        :columns="columns"
        :dataSource="dataSource"
        :pagination="ipagination"
        :loading="loading"
        :rowSelection="{selectedRowKeys: selectedRowKeys, onChange: onSelectChange}"
        class="j-table-force-nowrap"
        @change="handleTableChange">

        <template slot="htmlSlot" slot-scope="text">
          <div v-html="text"></div>
        </template>
        <template slot="imgSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无图片</span>
          <img v-else :src="getImgView(text)" height="25px" alt="" style="max-width:80px;font-size: 12px;font-style: italic;"/>
        </template>
        <template slot="fileSlot" slot-scope="text">
          <span v-if="!text" style="font-size: 12px;font-style: italic;">无文件</span>
          <a-button
            v-else
            :ghost="true"
            type="primary"
            icon="download"
            size="small"
            @click="downloadFile(text)">
            下载
          </a-button>
        </template>

<!--        <span slot="action" slot-scope="text, record">-->
<!--          <a @click="handleEdit(record)">编辑</a>-->

<!--          <a-divider type="vertical" />-->
<!--          <a-dropdown>-->
<!--            <a class="ant-dropdown-link">更多 <a-icon type="down" /></a>-->
<!--            <a-menu slot="overlay">-->
<!--              <a-menu-item>-->
<!--                <a @click="handleDetail(record)">详情</a>-->
<!--              </a-menu-item>-->
<!--              <a-menu-item>-->
<!--                <a-popconfirm title="确定删除吗?" @confirm="() => handleDelete(record.id)">-->
<!--                  <a>删除</a>-->
<!--                </a-popconfirm>-->
<!--              </a-menu-item>-->
<!--            </a-menu>-->
<!--          </a-dropdown>-->
<!--        </span>-->

        <template slot="customRenderStatus" slot-scope="status">
          <a-tag v-if="status==0" color="red">{{$t('未支付')}}</a-tag>
          <a-tag v-if="status==1" color="green">{{$t('已支付')}}</a-tag>
        </template>
        <template slot="customRenderIsDiscount" slot-scope="isDiscount">
          <a-tag v-if="isDiscount==0" color="gray">{{$t('否')}}</a-tag>
          <a-tag v-if="isDiscount==1" color="orange">{{$t('是')}}</a-tag>
        </template>

      </a-table>

      <div class="ant-col ant-form-item-label"><label class="">{{$t('交易总价')}}</label>{{totalFee}} </div>

    </div>

    <show-order-modal ref="modalForm" @ok="modalFormOk"></show-order-modal>
  </a-card>
</template>

<script>

  import '@/assets/less/TableExpand.less'
  import { mixinDevice } from '@/utils/mixin'
  import { JeecgListMixin } from '@/mixins/JeecgListMixin'
  import ShowOrderModal from './modules/ShowOrderModal'
  import {getAction} from "@api/manage";

  export default {
    name: 'ShowOrderList',
    mixins:[JeecgListMixin, mixinDevice],
    components: {
      ShowOrderModal
    },
    data () {
      return {
        description: '订单管理管理页面',
        totalFee: 0,
        // 表头
        columns: [
          {
            title: '#',
            dataIndex: '',
            key:'rowIndex',
            width:60,
            align:"center",
            customRender:function (t,r,index) {
              return parseInt(index)+1;
            }
          },
          {
            title:this.$t('平台'),
            align:"center",
            dataIndex: 'platform'
          },
          {
            title:this.$t('订单类型'),
            align:"center",
            dataIndex: 'orderType'
          },
          {
            title:this.$t('订单号'),
            align:"center",
            dataIndex: 'orderId'
          },
          {
            title:this.$t('付费时间'),
            align:"center",
            dataIndex: 'payTime',
            customRender:function (text) {
              return !text?"":(text.length>19?text.substr(0,19):text)
            }
          },
          {
            title: this.$t('展馆'),
            align: "center",
            width: 100,
            dataIndex: 'showMuseumId_dictText'
          },
          {
            title: this.$t('房间'),
            align: "center",
            width: 100,
            dataIndex: 'showRoomId_dictText'
          },
          {
            title:this.$t('账号'),
            align:"center",
            dataIndex: 'showUserEmail'
          },
          {
            title:this.$t('折扣码是否使用'),
            align:"center",
            dataIndex: 'isDiscount',
            scopedSlots: { customRender: 'customRenderIsDiscount' },
            filterMultiple: false
          },
          {
            title:this.$t('付费价钱'),
            align:"center",
            dataIndex: 'payFee'
          },
          {
            title:this.$t('状态'),
            align:"center",
            dataIndex: 'status',
            scopedSlots: { customRender: 'customRenderStatus' },
            filterMultiple: false
          }
        ],
        isorter:{
          column: 'orderTime',
          order: 'desc',
        },
        url: {
          list: "/order/showOrder/list",
          totalFee:"/order/showOrder/getTotalFee",
          delete: "/order/showOrder/delete",
          deleteBatch: "/order/showOrder/deleteBatch",
          exportXlsUrl: "/order/showOrder/exportXls",
          importExcelUrl: "order/showOrder/importExcel",

        },
        dictOptions:{},
        superFieldList:[],
      }
    },
    created() {
      this.getTotalFee();
    },
    computed: {
      importExcelUrl: function(){
        return `${window._CONFIG['domianURL']}/${this.url.importExcelUrl}`;
      },
    },
    methods: {
      initDictConfig(){
      },
      searchQuery() {
        this.loadData(1);
        this.getTotalFee();
      },
      searchReset() {
        this.queryParam = {}
        this.loadData(1);
        this.getTotalFee();
      },
      getTotalFee(){
        var params = this.getQueryParams();//查询条件
        this.loading = true;
        getAction(this.url.totalFee, params).then((res) => {
          if (res.success) {
            this.totalFee = res.result;
          }else{
            this.$message.warning(res.message)
          }
        }).finally(() => {
          this.loading = false
        })
      }
    }
  }
</script>
<style scoped>
  @import '~@assets/less/common.less';
</style>